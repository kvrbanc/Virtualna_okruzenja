package virokr.repositories;

import org.springframework.data.jpa.repository.JpaRepository;

import virokr.models.HighScore;

public interface ScoreRepository extends JpaRepository<HighScore, Integer> {
	
}
