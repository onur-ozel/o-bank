package com.controllers;

import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

/**
 * TimeDeposit
 */
@RestController
@RequestMapping("/TimeDeposit")
public class TimeDepositContoller {

    @RequestMapping("/items")
    public String name() {
        return "AAAA";
    }
}

