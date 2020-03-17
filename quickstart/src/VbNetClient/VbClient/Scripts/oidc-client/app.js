function log() {
    //document.getElementById('results').innerText = '';

    //Array.prototype.forEach.call(arguments, function (msg) {
    //    if (msg instanceof Error) {
    //        msg = "Error: " + msg.message;
    //    }
    //    else if (typeof msg !== 'string') {
    //        msg = JSON.stringify(msg, null, 2);
    //    }
    //    document.getElementById('results').innerHTML = msg;
    //});

}

//document.getElementById("login").addEventListener("click", login, false);
//document.getElementById("api").addEventListener("click", api, false);
//document.getElementById("authApi").addEventListener("click", authApi, false);
//document.getElementById("logout").addEventListener("click", logout, false);
//document.getElementById("silent").addEventListener("click", silent);

var config = {
    authority: "http://localhost:5000",
    client_id: "vbnet",
    redirect_uri: "http://localhost:5005/callback.html",
    response_type: "code",
    scope: "openid profile email afcpayroll",
    post_logout_redirect_uri: "http://localhost:5005/Default.aspx",
    //automaticSilentRenew: false,
    //silent_redirect_uri: "http://localhost:5005/silent.html"
};
var mgr = new Oidc.UserManager(config);



//mgr.startSilentRenew();


var refreshCount = 0;
var refreshLimit = 4;

mgr.getUser().then(function (user) {
    if (user) {
        console.log(user);

        //console.log(user.expires_at);
        console.log(user.expired);

        console.log("User logged in", user.profile);
    }
    else {
        console.log("User not logged in");
        ///login();
    }

    console.log('href ' + window.location.href); //returns the href (URL) of the current page
    console.log('hostname ' + window.location.hostname); //returns the domain name of the web host
    console.log('path name ' + window.location.pathname); //returns the path and filename of the current page
    console.log('protocol ' + window.location.protocol); //returns the web protocol used (http: or https:)
    //console.log('' + window.location.assign());
});

function login() {
    console.log('login called');
    mgr.signinRedirect();
}

function loadApiData(user) {
    //mgr.getUser().then(function (user) {
    //    console.log(user);
    //    //console.log(user.expires_at);
    //    console.log(user.expired);

        var url = "http://localhost:5001/identity";

        var xhr = new XMLHttpRequest();
        xhr.open("GET", url);
        xhr.onload = function () {
            log(xhr.status, JSON.parse(xhr.responseText));
        }
        xhr.setRequestHeader("Authorization", "Bearer " + user.access_token);
        xhr.send();
    //});
}

function api() {
    mgr.getUser().then(function (user) {
        console.log(user);
        //console.log(user.expires_at);
        console.log(user.expired);
        if (user.expired) {
            mgr.signinSilent().then(function (user) {
                console.log("silent renew success! and loading data", user);
                loadApiData(user);
            }).catch(function (err) {
                console.log(err);
                login();
            });
        }
        else {
            loadApiData(user);
        }
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
function silent() {
    mgr.signinSilent().then(function (user) {
        refreshCount++;
        console.log('refresh count: ' + refreshCount);
        console.log(user);
    }).catch(function (err) {
        logout();
    });
}

function logout() {
    mgr.signoutRedirect();
}

mgr.events.addAccessTokenExpired(function () {

    if (refreshCount < refreshLimit) {
        silent();
        console.log('silent refresh about to happen!');
    }
    else {
        console.log('refresh limit exceed');
        var go = confirm('Would like stay login?');
        console.log(go);

        if(go){
            logout();
        }
        else {
            refreshCount = 0;
        }
    }
});

mgr.events.addUserSignedOut(function () {
    console.log("user is now signed out of the token server");
});

