export default {
    formatUnicorn(text, params) {
        console.log(params)
        for (let key in params) {
            text = text.replace("{" + key + "}", params[key]);
        }

        return text;
    }
}