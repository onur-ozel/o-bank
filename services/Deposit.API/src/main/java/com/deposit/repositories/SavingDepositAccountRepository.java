package com.deposit.repositories;

import com.deposit.models.SavingDepositAccount;

import org.springframework.data.repository.PagingAndSortingRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface SavingDepositAccountRepository extends PagingAndSortingRepository<SavingDepositAccount, String> {

}