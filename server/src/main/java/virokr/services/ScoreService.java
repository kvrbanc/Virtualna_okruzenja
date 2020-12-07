package virokr.services;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.stereotype.Service;

import virokr.models.AuthUser;
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
	
	public HighScore addScore(String username, int value) {
		AuthUser user = getUserFromUsername(username);
		HighScore score = null;
		List<HighScore> scores = scoreRepo.findAll();
		for (HighScore s : scores) {
			if (user.getId() == s.getUser().getId()) {
				score = s;
				break;
			}
		}
		if (score == null) {
			score = new HighScore(user, value);
		} else if (value > score.getValue()) {
			score.setValue(value);
		}
		scoreRepo.save(score);
		return score;
	}

	private AuthUser getUserFromUsername(String username) {
		return userRepo.findByUsername(username)
				.orElseThrow(() -> new UsernameNotFoundException("Korisnik s imenom " + username + " nije pronađen."));
	}
}
