package virokr.repositories;

import java.util.Optional;

import org.springframework.data.jpa.repository.JpaRepository;

import virokr.models.AuthUser;

public interface UserRepository extends JpaRepository<AuthUser, Integer> {
    Optional<AuthUser> findByUsername(String username);

	Boolean existsByUsername(String username);
}
