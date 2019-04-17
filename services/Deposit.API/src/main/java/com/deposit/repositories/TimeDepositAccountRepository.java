package com.deposit.repositories;

import com.deposit.models.TimeDepositAccount;

import org.springframework.data.repository.PagingAndSortingRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface TimeDepositAccountRepository extends PagingAndSortingRepository<TimeDepositAccount, String> {

}