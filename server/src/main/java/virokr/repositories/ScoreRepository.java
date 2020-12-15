package virokr.repositories;

import org.springframework.data.jpa.repository.JpaRepository;

import virokr.models.Score;

public interface ScoreRepository extends JpaRepository<Score, Integer> {
	
}
