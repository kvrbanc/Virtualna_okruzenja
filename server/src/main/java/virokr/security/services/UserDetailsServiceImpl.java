package virokr.security.services;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import virokr.models.AuthUser;
import virokr.repositories.UserRepository;

@Service
public class UserDetailsServiceImpl implements UserDetailsService {
	@Autowired
	UserRepository userRepository;

	@Override
	@Transactional
	public UserDetails loadUserByUsername(String username) throws UsernameNotFoundException {
		AuthUser user = userRepository.findByUsername(username)
				.orElseThrow(() -> new UsernameNotFoundException("Korisnik s imenom " + username + " nije pronađen."));

		return UserDetailsImpl.build(user);
	}

}