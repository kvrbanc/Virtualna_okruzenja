package virokr.components;

import java.util.Arrays;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.ApplicationArguments;
import org.springframework.boot.ApplicationRunner;
import org.springframework.stereotype.Component;

import virokr.repositories.RoleRepository;
import virokr.models.AuthRole;

@Component
public class Startup implements ApplicationRunner {

    @Autowired
    RoleRepository roleRepo;

    @Override
    public void run(ApplicationArguments args) throws Exception {

        if (roleRepo.findAll().size() == 0) {
            roleRepo.saveAll(Arrays.asList(new AuthRole(1, "ADMIN"), new AuthRole(2, "USER")));
        }
    }
}
