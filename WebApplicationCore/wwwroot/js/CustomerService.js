var CustomerService = function () {

    var save = function (masterObj, done, fail) {

        $.ajax({
            url: '/Receipt/CreateEdit',
            method: 'post',
            data: masterObj

        })
            .done(done)
            .fail(fail);

    };

    return {
        save: save
    }

}();