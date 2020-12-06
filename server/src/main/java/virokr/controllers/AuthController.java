package virokr.controllers;

import java.util.HashSet;
import java.util.List;
import java.util.Set;
import java.util.stream.Collectors;

import javax.validation.Valid;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import virokr.models.AuthRole;
import virokr.models.AuthUser;
import virokr.payload.request.LoginRequest;
import virokr.payload.request.RegisterRequest;
import virokr.payload.response.JwtResponse;
import virokr.payload.response.MessageResponse;
import virokr.security.services.UserDetailsImpl;
import virokr.repositories.UserRepository;
import virokr.repositories.RoleRepository;
import virokr.security.jwt.JwtUtils;


@RestController
@CrossOrigin(origins={"*"})
@RequestMapping("/auth")
public class AuthController {
	
	@Autowired
	AuthenticationManager authenticationManager;
	
	@Autowired
	UserRepository userRepo;
	
	@Autowired
	RoleRepository roleRepo;
	
	@Autowired
	PasswordEncoder encoder;
	
	@Autowired
	JwtUtils jwtUtils;
	
	@PostMapping("/login")
	public ResponseEntity<?> authenticateUser(@Valid @RequestBody LoginRequest loginRequest) {
		
		Authentication authentication = authenticationManager.authenticate(
		new UsernamePasswordAuthenticationToken(loginRequest.getUsername(), loginRequest.getPassword()));
		
		SecurityContextHolder.getContext().setAuthentication(authentication);
		String jwt = jwtUtils.generateJwtToken(authentication);
		
		UserDetailsImpl userDetails = (UserDetailsImpl) authentication.getPrincipal();		
		List<String> roles = userDetails.getAuthorities().stream()
		.map(item -> item.getAuthority())
		.collect(Collectors.toList());
		
		return ResponseEntity.ok(new JwtResponse(jwt, 
		userDetails.getId(), 
		userDetails.getUsername(),
		roles));
	}
	
	@PostMapping("/register")
	public ResponseEntity<?> registerUser(@Valid @RequestBody RegisterRequest registerRequest) {
		if (userRepo.existsByUsername(registerRequest.getUsername())) {
			return ResponseEntity
			.badRequest()
			.body(new MessageResponse("Greška: Korisničko ime je već zauzeto!"));
		}
		
		// Create new user's account
		AuthUser user = new AuthUser(registerRequest.getUsername(),
		encoder.encode(registerRequest.getPassword()));
		
		Set<String> strRoles = registerRequest.getRole();
		Set<AuthRole> roles = new HashSet<>();
		
		if (strRoles == null) {
			AuthRole userRole = roleRepo.findByLabel("USER")
			.orElseThrow(() -> new RuntimeException("Greška: Uloga nije pronađena."));
			roles.add(userRole);
		} else {
			strRoles.forEach(role -> {
				switch (role) {
					case "admin":
					AuthRole adminRole = roleRepo.findByLabel("ADMIN")
					.orElseThrow(() -> new RuntimeException("Greška: Admin uloga nije pronađena."));
					roles.add(adminRole);
					
					break;
					default:
					AuthRole userRole = roleRepo.findByLabel("USER")
					.orElseThrow(() -> new RuntimeException("Greška: Uloga nije pronađena."));
					roles.add(userRole);
				}
			});
		}
		
		user.setRoles(roles);
		userRepo.save(user);
		
		return ResponseEntity.ok(new MessageResponse("Korisnik uspješno registriran!"));
	}
}