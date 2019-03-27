package com.deposit.repositories;

import com.deposit.models.WithdrawDepositAccount;

import org.springframework.data.repository.PagingAndSortingRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface WithdrawDepositAccountRepository extends PagingAndSortingRepository<WithdrawDepositAccount, String> {

}