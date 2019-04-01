package com.deposit.controllers;

import java.io.IOException;

import javax.servlet.http.HttpServletResponse;

import org.springframework.core.io.ClassPathResource;
import org.springframework.core.io.Resource;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class SwaggerController {
    @RequestMapping(method = RequestMethod.GET, path = "/deposit/swagger")
    public void home(HttpServletResponse httpResponse) throws IOException {
        httpResponse.sendRedirect("/swagger-ui.html");
    }

    @RequestMapping(method = RequestMethod.GET, path = "/v2/api-docs", produces = "application/yaml")
    public Resource apiDocs() {
        return new ClassPathResource("../resources/swagger/swagger.yaml");
    }

    @RequestMapping(method = RequestMethod.GET, path = "/swagger-resources/configuration/ui", produces = "application/json")
    public Object uiConfig() {
        return new ClassPathResource("../resources/swagger/swagger-config-ui.json");
    }

    @RequestMapping(method = RequestMethod.GET, path = "/swagger-resources/configuration/security", produces = "application/json")
    public Object securityConfig() {
        return "{}";
    }

    @RequestMapping(method = RequestMethod.GET, path = "/swagger-resources", produces = "application/json")
    public Object resources() {
        return new ClassPathResource("../resources/swagger/swagger-resources.json");
    }
}
