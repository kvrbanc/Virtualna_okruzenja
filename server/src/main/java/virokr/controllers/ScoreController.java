package virokr.controllers;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestHeader;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import io.swagger.v3.oas.annotations.parameters.RequestBody;
import virokr.models.HighScore;
import virokr.security.jwt.JwtUtils;
import virokr.services.ScoreService;

@RestController
@CrossOrigin(origins={"*"})
@RequestMapping("/scores")
public class ScoreController {

    @Autowired
	ScoreService scoreService;
	
	@Autowired
	JwtUtils jwtUtils;

    @GetMapping("")
	public List<HighScore> getScores() {
		return scoreService.getScores();
	}
	
    @GetMapping("/{id}")
	public HighScore getScoreById(@PathVariable int id) {
		return scoreService.getScoreById(id);
	}

	@DeleteMapping("/{id}")
	@PreAuthorize("hasAuthority('ADMIN')")
	public void deleteScoreById(@PathVariable int id) {
		scoreService.deleteScoreById(id);
	}

	@PostMapping("/")
	public ResponseEntity<Object> addScore(@RequestHeader(value = "Authorization") String headerAuth, @RequestBody int value) {
		try {
			scoreService.addScore(jwtUtils.getUsernameFromHeader(headerAuth), value);
			return new ResponseEntity<>(HttpStatus.OK);
		} catch (Exception e) {
			return new ResponseEntity<>(e.getMessage(), HttpStatus.BAD_REQUEST);
		}
	}
}