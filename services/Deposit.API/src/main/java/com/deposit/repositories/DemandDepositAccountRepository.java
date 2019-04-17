package com.deposit.repositories;

import com.deposit.models.DemandDepositAccount;

import org.springframework.data.repository.PagingAndSortingRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface DemandDepositAccountRepository extends PagingAndSortingRepository<DemandDepositAccount, String> {

}