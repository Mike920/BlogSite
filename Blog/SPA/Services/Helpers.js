'use strict';

/* Services */

angular
    .module('app')
    .factory('Helpers', Helpers);

function Helpers() {
    var error = function(msg) {
        return { msg: msg, clas: "alert-error" };
    }
    var success = function (msg) {
        return { msg: msg, clas: "alert-success" };
    }

    return {
        error: error,
        success: success
    };
};
