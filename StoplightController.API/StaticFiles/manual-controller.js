manualController = {
    toggleRed: function() {
        $.post('/api/manual/toggle-red');
    },

    toggleYellow: function () {
        $.post('/api/manual/toggle-yellow');
    },

    toggleGreen: function() {
        $.post('/api/manual/toggle-green');
    }
}