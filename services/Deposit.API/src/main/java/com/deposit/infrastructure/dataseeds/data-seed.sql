 CREATE TABLE withdraw_deposit_account
(
    id varchar(100),
    customer_number bigint,
    account_number bigint,
    balance double,
    overdraft_limit double
);

insert into withdraw_deposit_account (id,customer_number,account_number,balance,overdraft_limit)
        values ('1111',1111,2222,123456,111);
insert into withdraw_deposit_account (id,customer_number,account_number,balance,overdraft_limit)
        values ('1112',1112,2223,123457,111);
insert into withdraw_deposit_account (id,customer_number,account_number,balance,overdraft_limit)
        values ('1113',1113,2224,123458,112);