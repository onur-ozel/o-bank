package com.deposit.services;

import java.util.List;
import java.util.Optional;

import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;
import javax.persistence.TypedQuery;
import javax.persistence.criteria.CriteriaBuilder;
import javax.persistence.criteria.CriteriaQuery;
import javax.persistence.criteria.Root;

import com.deposit.infrastructure.utils.ApiUtils;
import com.deposit.infrastructure.utils.Json;
import com.deposit.models.WithdrawDepositAccount;
import com.deposit.repositories.WithdrawDepositAccountRepository;
import com.fasterxml.jackson.core.JsonProcessingException;

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

    @PersistenceContext
    private EntityManager em;

    public String get(Integer offset, Integer limit, String sorts, String fields, String searches)
            throws JsonProcessingException {
        Boolean hasPaging = offset != null && limit != null;
        Boolean hasSort = sorts != null && !sorts.isEmpty();
        Boolean hasSearch = searches != null && !searches.isEmpty();

        List<WithdrawDepositAccount> returnData;

        CriteriaBuilder cb = em.getCriteriaBuilder();

        CriteriaQuery<WithdrawDepositAccount> cq = cb.createQuery(WithdrawDepositAccount.class);
        Root<WithdrawDepositAccount> account = cq.from(WithdrawDepositAccount.class);

        cq.select(account);

        if (hasSort) {
            cq.orderBy(ApiUtils.parseSort(sorts, cb, account));
        }

        if (hasSearch) {
            cq.where(ApiUtils.parseSearches(searches, cb, account));
        }

        TypedQuery<WithdrawDepositAccount> typedQuery = em.createQuery(cq);

        if (hasPaging) {
            typedQuery.setFirstResult(offset);
            typedQuery.setMaxResults(limit);
        }

        returnData = typedQuery.getResultList();

        Json.serializer().setFilterProvider(ApiUtils.parseFields(fields, "withdrawDepositFilter"));

        return Json.serializer().toString(returnData);
    }

    public Optional<WithdrawDepositAccount> getById(String id) {
        return (Optional<WithdrawDepositAccount>) repository.findById(id);
    }

}
