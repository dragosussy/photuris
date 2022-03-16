import endpoints from './../endpoints.json'

const getCookieValue = (name) => (
  document.cookie.match('(^|;)\\s*' + name + '\\s*=\\s*([^;]+)')?.pop() || ''
)

const LoginUtils = {
    isLoggedInEndpoint: endpoints.checkIsLoggedIn,

    getSessionCookieValue: function() {
        return getCookieValue("auth_token");
    },
    isLoggedIn: async function() {
        let sessionToken = getCookieValue("auth_token");
        if(sessionToken.length === 0) return false;

        return await fetch(`${this.isLoggedInEndpoint}?token=${sessionToken}`, {method: "GET", mode: "cors"})
                        .then((response) => response.text())
                        .then(isLoggedInResponseText => {
                            let isLoggedInBoolean = (isLoggedInResponseText === 'true');
                            return isLoggedInBoolean;
                        });
    }
}

export default LoginUtils;