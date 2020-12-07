package virokr.services;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.stereotype.Service;

import virokr.exceptions.InvalidOwnershipException;
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

	public HighScore addScore(String username, int id, int value) throws InvalidOwnershipException {
		if (!checkOwnership(username, id))
			throw new InvalidOwnershipException("Korisničko ime " + username + " nije povezano s id-em " + id + ".");
		HighScore score = null;
		List<HighScore> scores = scoreRepo.findAll();
		for (HighScore s : scores) {
			if (id == s.getUser().getId()) {
				score = s;
				break;
			}
		}
		if (score == null) {
			score = new HighScore(userRepo.getOne(id), value);
		} else if (value > score.getValue()) {
			score.setValue(value);
		}
		scoreRepo.save(score);
		return score;
	}

	private boolean checkOwnership(String username, int id) {
		AuthUser user = userRepo.findByUsername(username)
				.orElseThrow(() -> new UsernameNotFoundException("Korisnik s imenom " + username + " nije pronađen."));
		return user.getId() == id;
	}
}
