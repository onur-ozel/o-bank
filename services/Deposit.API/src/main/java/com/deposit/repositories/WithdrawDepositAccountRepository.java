package com.deposit.repositories;

import com.deposit.models.WithdrawDepositAccount;

import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface WithdrawDepositAccountRepository extends CrudRepository<WithdrawDepositAccount, String> {

}