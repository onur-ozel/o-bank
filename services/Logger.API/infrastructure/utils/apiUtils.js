exports.dynamicSelect = (fields, queryOptions) => {
    if (!fields) {
        return queryOptions;
    }

    var fieldsArray = fields.split(',');
    queryOptions.select = fieldsArray;

    return queryOptions;
};

exports.dynamicWhere = (searches, query) => {
    if (!searches) {
        return query;
    }

    var searchesArray = searches.split(',');

    for (let i = 0; i < searchesArray.length; i++) {
        var searchValue = "";
        var searchField = "";
        var searchExpression = "";

        searchField = searchesArray[i].split(/\[.*]/)[0];
        searchValue = searchesArray[i].split(/\[.*]/)[1];
        searchExpression = searchesArray[i].substring(searchField.length, searchesArray[i].indexOf(searchValue)).replace(/[\[\]]/g, '')

        var expression = {};

        switch (searchExpression) {
            case "=":
                expression = { '$eq': searchValue };
                break;
            case "!=":
                expression = { '$ne': searchValue };
                break;
            case ">=":
                expression = { '$gte': searchValue };
                break;
            case "<=":
                expression = { '$lte': searchValue };
                break;
            case ">":
                expression = { '$gt': searchValue };
                break;
            case "<":
                expression = { '$lt': searchValue };
                break;
            default:
                throw new Exception("Undefined search parameter");
        }

        query[searchField] = expression;
    }
    console.log(query);

    // selectQuery.select = fieldsArray;

    return query;
};
