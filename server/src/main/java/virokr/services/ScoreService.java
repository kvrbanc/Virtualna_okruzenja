package virokr.services;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.stereotype.Service;

import virokr.models.AuthUser;
import virokr.models.Score;
import virokr.repositories.ScoreRepository;
import virokr.repositories.UserRepository;

@Service
public class ScoreService {

	@Autowired
	ScoreRepository scoreRepo;

	@Autowired
	UserRepository userRepo;

	public List<Score> getScores() {
		return scoreRepo.findAll();
	}

	public Score getScoreById(int id) {
		return scoreRepo.findById(id).get();
	}

	public void deleteScoreById(int id) {
		scoreRepo.deleteById(id);
	}
	
	public Score addScore(String username, int value) {
		if (value < 0 || value > 666) throw new IllegalArgumentException("Rezultat izvan ograničenja [0-666]");
		AuthUser user = getUserFromUsername(username);
		Score score = null;
		List<Score> scores = scoreRepo.findAll();
		for (Score s : scores) {
			if (user.getId() == s.getUser().getId()) {
				score = s;
				break;
			}
		}
		if (score == null) {
			score = new Score(user, value);
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

	public int getScoreRank(int id) {
		List<Score> scores = getScores();
		int i = 1;
		for (Score score : scores) {
			if (score.getId() == id) {
				return i;
			}
			i++;
		}
		return 0;
	}
}
