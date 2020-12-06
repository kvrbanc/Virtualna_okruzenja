package virokr.models;

import java.util.Set;

import javax.persistence.CascadeType;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.FetchType;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.JoinTable;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToMany;
import javax.persistence.OneToOne;
import javax.persistence.Table;
import javax.validation.constraints.Size;

import com.fasterxml.jackson.annotation.JsonIgnore;
import com.fasterxml.jackson.annotation.JsonIgnoreProperties;

import org.springframework.data.rest.core.annotation.RestResource;

@Entity
@Table(name = "auth_user")
@RestResource(rel = "users", path = "users")
public class AuthUser {
    
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int id;

    @JsonIgnoreProperties("user")
    @OneToOne(mappedBy = "user", cascade = {CascadeType.DETACH, CascadeType.MERGE, CascadeType.REFRESH, CascadeType.REMOVE}) 
    private HighScore highScore;

    @Column(name = "username", nullable = false, unique = true)
    @Size(min = 3, max = 15)
    private String username;
    
    @JsonIgnore
    @Column(name = "password", nullable = false)
    private String password;

    @ManyToMany(fetch = FetchType.LAZY)
    @JoinTable(name = "auth_user_role", joinColumns = @JoinColumn(name = "user_id"), inverseJoinColumns = @JoinColumn(name = "role_id"))
    private Set<AuthRole> roles;

    public AuthUser() {
    }

    public AuthUser(String username, String password) {
        this.username = username;
        this.password = password;
    }
    
    public int getId() {
        return id;
    }
    
    public void setId(int id) {
        this.id = id;
    }
    
    public HighScore getHighScore() {
        return highScore;
    }

    public void setHighScore(HighScore highScore) {
        this.highScore = highScore;
    }
    
    public String getUsername() {
        return username;
    }
    
    public void setUsername(String username) {
        this.username = username;
    }
    
    public String getPassword() {
        return password;
    }
    
    public void setPassword(String password) {
        this.password = password;
    }
    
    public Set<AuthRole> getRoles() {
        return roles;
    }
    
    public void setRoles(Set<AuthRole> roles) {
        this.roles = roles;
    }
}