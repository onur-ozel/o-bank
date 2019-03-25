package com.deposit.models;

import javax.persistence.Column;
import javax.persistence.Entity;

@Entity
public abstract class DepositAccount {

    @Column(name = "id")
    private String id;
    @Column(name = "customer_number")
    private Long customerNumber;
    @Column(name = "account_number")
    private Long accountNumber;
    @Column(name = "balance")
    private Double balance;

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public Long getCustomerNumber() {
        return customerNumber;
    }

    public void setCustomerNumber(Long customerNumber) {
        this.customerNumber = customerNumber;
    }

    public Long getAccountNumber() {
        return accountNumber;
    }

    public void setAccountNumber(Long accountNumber) {
        this.accountNumber = accountNumber;
    }

    public Double getBalance() {
        return balance;
    }

    public void setBalance(Double balance) {
        this.balance = balance;
    }
}