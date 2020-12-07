package virokr.controllers;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import virokr.models.AuthUser;
import virokr.services.UserService;

@RestController
@CrossOrigin(origins={"*"})
@RequestMapping("/users")
public class UserController {

	@Autowired
	UserService userService;

    @GetMapping("")
	public List<AuthUser> getUsers() {
        return userService.getUsers();
	}

	@GetMapping("/{id}")
	public AuthUser getUserById(@PathVariable int id) {
		return userService.getUserById(id);
	}

	@PreAuthorize("hasAuthority('ADMIN')")
	@DeleteMapping("/{id}")
	public void deleteUserById(@PathVariable int id) {
        userService.deleteUserById(id);
	}
}


