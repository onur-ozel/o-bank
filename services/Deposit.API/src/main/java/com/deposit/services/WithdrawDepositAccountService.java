package com.deposit.services;

import java.util.List;
import java.util.Optional;

import com.deposit.models.WithdrawDepositAccount;
import com.deposit.repositories.WithdrawDepositAccountRepository;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

@Component
public class WithdrawDepositAccountService {
    @Autowired
    WithdrawDepositAccountRepository repository;

    public void add(WithdrawDepositAccount account) {
        repository.save(account);
    }

    public void delete(String id) {
        repository.deleteById(id);
    }

    public List<WithdrawDepositAccount> get() {
        // repository.

        return (List<WithdrawDepositAccount>) repository.findAll();
    }

    public Optional<WithdrawDepositAccount> getById(String id) {
        return (Optional<WithdrawDepositAccount>) repository.findById(id);
    }

}