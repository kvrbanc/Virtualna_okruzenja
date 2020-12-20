package virokr.services;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import virokr.models.AuthUser;
import virokr.models.Score;
import virokr.repositories.UserRepository;

@Service
public class UserService {

    @Autowired
    UserRepository userRepo;

    public List<AuthUser> getUsers() {
        return userRepo.findAll();
    }

    public void deleteUserById(int id) {
        userRepo.deleteById(id);
    }

    public AuthUser getUserById(int id) {
        return userRepo.findById(id).get();
    }

    public Score getUserScore(int id) {
         return userRepo.findById(id).get().getScore();
    }
}