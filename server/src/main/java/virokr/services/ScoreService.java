package virokr.services;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import virokr.models.HighScore;
import virokr.repositories.ScoreRepository;
import virokr.repositories.UserRepository;

@Service
public class ScoreService {

	@Autowired
	ScoreRepository scoreRepo;

	@Autowired
	UserRepository userRepo;

	public List<HighScore> getScores() {
		return scoreRepo.findAll();
	}

	public HighScore getScoreById(int id) {
		return scoreRepo.findById(id).get();
	}

	public void deleteScoreById(int id) {
		scoreRepo.deleteById(id);
	}
}
