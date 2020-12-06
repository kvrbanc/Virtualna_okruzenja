package virokr.repositories;

import java.util.Optional;

import org.springframework.data.jpa.repository.JpaRepository;

import virokr.models.AuthRole;

public interface RoleRepository extends JpaRepository<AuthRole, Integer> {
	
	Optional<AuthRole> findByLabel(String label);
	
}
