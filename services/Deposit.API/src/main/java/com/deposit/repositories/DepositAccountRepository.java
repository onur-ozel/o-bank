package com.deposit.repositories;

import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface DepositAccountRepository extends CrudRepository<System, Long> {

}