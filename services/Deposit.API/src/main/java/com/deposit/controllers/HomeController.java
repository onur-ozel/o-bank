package com.deposit.controllers;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;

@Controller // note - this is a spring-boot controller, not @RestController
public class HomeController {
    @RequestMapping ("/swagger")
    public String home() {
	return "redirect:/swagger-ui.html";
    }
}