package com.deposit.controllers;

import java.util.Optional;

import com.deposit.models.WithdrawDepositAccount;
import com.deposit.services.WithdrawDepositAccountService;
import com.fasterxml.jackson.core.JsonProcessingException;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.PathVariable;
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
    public String get(@RequestParam(name = "offset", required = false) Integer offset,
            @RequestParam(name = "limit", required = false) Integer limit,
            @RequestParam(name = "sorts", required = false) String sorts,
            @RequestParam(name = "fields", required = false) String fields,
            @RequestParam(name = "searches", required = false) String searches) throws JsonProcessingException {

        return service.get(offset, limit, sorts, fields, searches);
    }

    @RequestMapping(value = "", method = RequestMethod.POST, produces = "application/json")
    public void add(@RequestBody WithdrawDepositAccount account) {
        service.add(account);
    }

    @RequestMapping(value = "", method = RequestMethod.PUT, produces = "application/json")
    public void update(@RequestBody WithdrawDepositAccount account) {
        service.update(account);
    }

    @RequestMapping(value = "/{id}", method = RequestMethod.DELETE, produces = "application/json")
    public void delete(@PathVariable(name = "id", required = true) String id) {
        service.delete(id);
    }

    @RequestMapping(value = "/{id}", method = RequestMethod.GET, produces = "application/json")
    public Optional<WithdrawDepositAccount> getById(@PathVariable(name = "id", required = true) String id) {
        return service.getById(id);
    }
}
