package com.deposit.services;

import java.util.List;

import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;
import javax.persistence.TypedQuery;
import javax.persistence.criteria.CriteriaBuilder;
import javax.persistence.criteria.CriteriaQuery;
import javax.persistence.criteria.Root;

import com.deposit.infrastructure.utils.ApiUtils;
import com.deposit.infrastructure.utils.Json;
import com.deposit.models.SavingDepositAccount;
import com.deposit.repositories.SavingDepositAccountRepository;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class SavingDepositAccountService {
    @Autowired
    SavingDepositAccountRepository repository;

    @PersistenceContext
    private EntityManager em;

    public void add(SavingDepositAccount account) {
        repository.save(account);
    }

    public void delete(String id) {
        repository.deleteById(id);
    }

    public void update(SavingDepositAccount account) {
        SavingDepositAccount originalAccount = repository.findById(account.getId()).get();

        originalAccount = account;

        repository.save(originalAccount);
    }

    public String get(Integer offset, Integer limit, String sorts, String fields, String searches) throws Exception {
        Boolean hasPaging = offset != null && limit != null;
        Boolean hasSort = sorts != null && !sorts.isEmpty();
        Boolean hasSearch = searches != null && !searches.isEmpty();

        List<SavingDepositAccount> returnData;

        CriteriaBuilder cb = em.getCriteriaBuilder();

        CriteriaQuery<SavingDepositAccount> cq = cb.createQuery(SavingDepositAccount.class);
        Root<SavingDepositAccount> account = cq.from(SavingDepositAccount.class);

        cq.select(account);

        if (hasSort) {
            cq.orderBy(ApiUtils.parseSort(sorts, cb, account));
        }

        if (hasSearch) {
            cq.where(ApiUtils.parseSearches(searches, cb, account));
        }

        TypedQuery<SavingDepositAccount> typedQuery = em.createQuery(cq);

        if (hasPaging) {
            typedQuery.setFirstResult(offset);
            typedQuery.setMaxResults(limit);
        }

        returnData = typedQuery.getResultList();

        Json.serializer().setFilterProvider(ApiUtils.parseFields(fields, "savingDepositFilter"));

        return Json.serializer().toString(returnData);
    }

    public String getById(String id) {
        Json.serializer().setFilterProvider(ApiUtils.parseFields("", "savingDepositFilter"));

        return Json.serializer().toString(repository.findById(id).get());
    }
}
