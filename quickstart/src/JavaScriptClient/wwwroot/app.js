function log() {
    document.getElementById('results').innerText = '';

    Array.prototype.forEach.call(arguments, function (msg) {
        if (msg instanceof Error) {
            msg = "Error: " + msg.message;
        }
        else if (typeof msg !== 'string') {
            msg = JSON.stringify(msg, null, 2);
        }
        document.getElementById('results').innerHTML = msg;
    });
}

document.getElementById("login").addEventListener("click", login, false);
document.getElementById("api").addEventListener("click", api, false);
document.getElementById("authApi").addEventListener("click", authApi, false);
document.getElementById("logout").addEventListener("click", logout, false);


var config = {
    authority: "http://localhost:5000",
    client_id: "js",
    redirect_uri: "http://localhost:5003/callback.html",
    response_type: "code",
    scope: "openid profile afcpayroll offline_access",
    post_logout_redirect_uri: "http://localhost:5003/index.html"
};
var mgr = new Oidc.UserManager(config);
mgr.startSilentRenew();

mgr.getUser().then(function (user) {
    if (user) {
        console.log(user);

        console.log(user.expires_at);
        console.log(user.expired);

        log("User logged in", user.profile);
    }
    else {
        log("User not logged in");
    }
});

function login() {
    mgr.signinRedirect();
}

function api() {
    mgr.getUser().then(function (user) {
        
        console.log(user.expires_at);
        console.log(user.expired);
        
        var url = "http://localhost:5001/identity";

        var xhr = new XMLHttpRequest();
        xhr.open("GET", url);
        xhr.onload = function () {
            log(xhr.status, JSON.parse(xhr.responseText));
        }
        xhr.setRequestHeader("Authorization", "Bearer " + user.access_token);
        xhr.send();
    });
}

function authApi() {
    mgr.getUser().then(function (user) {
        var url = "http://localhost:5004/Home/PrivacyAsync";

        var xhr = new XMLHttpRequest();
        xhr.open("GET", url);
        xhr.onload = function () {
            //log(xhr.status, JSON.parse(xhr.responseText));

            document.getElementById('results').innerHTML = xhr.responseText;

        }
        //client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
        //xhr.setRequestHeader("Access-Control-Allow-Origin", "*");
        xhr.setRequestHeader("Authorization", "Bearer " + user.access_token);
        xhr.send();
    });
}
function logout() {
    mgr.signoutRedirect();
}