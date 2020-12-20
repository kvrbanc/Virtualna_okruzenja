package virokr.controllers;

import java.util.Collections;
import java.util.Comparator;
import java.util.List;
import java.util.stream.Collectors;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestHeader;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import virokr.models.Score;
import virokr.security.jwt.JwtUtils;
import virokr.services.ScoreService;

@RestController
@CrossOrigin(origins = { "*" })
@RequestMapping("/scores")
public class ScoreController {

	@Autowired
	ScoreService scoreService;

	@Autowired
	JwtUtils jwtUtils;

	@GetMapping("")
	public List<Score> getScores() {
		List<Score> scores = scoreService.getScores();
		Collections.sort(scores, new Comparator<Score>(){
			@Override
			public int compare(Score o1, Score o2) {
				return o2.getValue() - o1.getValue();
			}
			
		});
		return scores;
	}

	@GetMapping("/top")
	public List<Score> getTopScores() {
		List<Score> scores = getScores();
		return scores.stream().limit(10).collect(Collectors.toList());
	}

	@GetMapping("/{id}")
	public Score getScoreById(@PathVariable int id) {
		return scoreService.getScoreById(id);
	}

	@DeleteMapping("/{id}")
	@PreAuthorize("hasAuthority('ADMIN')")
	public void deleteScoreById(@PathVariable int id) {
		scoreService.deleteScoreById(id);
	}

	@PostMapping("")
	public ResponseEntity<Object> addScore(@RequestHeader(value = "Authorization") String headerAuth, @RequestBody Integer value) {
		try {
			scoreService.addScore(jwtUtils.getUsernameFromHeader(headerAuth), value);
			return new ResponseEntity<>(HttpStatus.OK);
		} catch (Exception e) {
			return new ResponseEntity<>(e.getMessage(), HttpStatus.BAD_REQUEST);
		}
	}
}