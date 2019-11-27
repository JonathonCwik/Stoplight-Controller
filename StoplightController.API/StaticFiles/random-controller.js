randomController = {
    start: function () {
        $.post('/api/random');
    },

    stop: function () {
        $.post('/api/stop');
    }
}