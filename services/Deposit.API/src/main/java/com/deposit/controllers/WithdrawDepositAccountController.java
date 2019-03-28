package com.deposit.controllers;

import com.deposit.models.WithdrawDepositAccount;
import com.deposit.services.WithdrawDepositAccountService;
import com.fasterxml.jackson.core.JsonProcessingException;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("deposit/api/v1/withdraw-deposit-accounts")
public class WithdrawDepositAccountController {

    @Autowired
    WithdrawDepositAccountService service;

    @RequestMapping(value = "", method = RequestMethod.GET, produces = "application/json")
    public String get(@RequestParam("offset") Integer offset, @RequestParam("limit") Integer limit,
            @RequestParam("sorts") String sorts, @RequestParam("fields") String fields,
            @RequestParam("searches") String searches) throws JsonProcessingException {

        return service.get(offset, limit, sorts, fields, searches);
    }

    @RequestMapping(value = "", method = RequestMethod.POST, produces = "application/json")
    public void add(@RequestBody WithdrawDepositAccount account) {
        service.add(account);
    }
}
