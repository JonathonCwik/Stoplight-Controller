$(function () {
    function startSocket() {
        var socket = new WebSocket("ws://" + window.location.host + "/ws");
        socket.onopen = function(event) {
            console.log(event);

            $.get('/current-state',
                function(data) {
                    console.log(data);
                    setState(data);
                });
        };
        socket.onerror = function(error) {
            console.log(error);
        };
        socket.onmessage = function(event) {

            var data = JSON.parse(event.data);

            setState(data);
        }

        socket.onclose = function(event) {
            console.log('Connection closed. Reopening.');
            socket = null;
            startSocket();
        }
    }

    startSocket();
});

function setState(state) {
    if (state.redIsOn) {
        $('.trafficlight .red').addClass("on");
    } else {
        $('.trafficlight .red').removeClass("on");
    }

    if (state.yellowIsOn) {
        $('.trafficlight .yellow').addClass("on");
    } else {
        $('.trafficlight .yellow').removeClass("on");
    }

    if (state.greenIsOn) {
        $('.trafficlight .green').addClass("on");
    } else {
        $('.trafficlight .green').removeClass("on");
    }
}