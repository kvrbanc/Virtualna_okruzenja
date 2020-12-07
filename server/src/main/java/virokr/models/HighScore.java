package virokr.models;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;
import javax.persistence.Table;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;

import org.springframework.data.rest.core.annotation.RestResource;

@Entity
@Table(name="game_score")
@RestResource(rel = "scores", path = "scores")
public class HighScore {

	@Id
	@GeneratedValue(strategy = GenerationType.IDENTITY)
	private int id;

	@ManyToOne
    @JsonIgnoreProperties({"scores", "form"})
	@JoinColumn(name = "user_id", nullable = false)
	private AuthUser user;
	
	@Column(name = "value", nullable = false)
	private int value;

	public HighScore(AuthUser user, int value) {
		this.user = user;
		this.value = value;
	}

	public int getId() {
		return id;
	}
	
	public void setId(int id) {
		this.id = id;
	}
	
	public AuthUser getUser() {
		return user;
	}
	
	public void setUser(AuthUser user) {
		this.user = user;
	}
	
	public int getValue() {
		return value;
	}
	
	public void setValue(int value) {
		this.value = value;
	}
}
