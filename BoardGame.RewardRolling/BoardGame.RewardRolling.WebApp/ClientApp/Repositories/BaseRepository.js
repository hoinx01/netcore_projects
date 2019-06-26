import axios from 'axios';
import { jsonUrls } from './UrlConstants'

const baseRepository = {
    token: null,
    async uploadFile(url, form) {
        let requestOptions = this.setRequestOptions(null);
        requestOptions.headers['Content-Type'] = "multipart/form-data";

        try {
            let response = await axios.post(url, form, requestOptions);
            return this.processRequestResponse(response);
        }
        catch (exception) {
            return Promise.reject(exception.response.data);
        }
    },
    async upload(file, fileGroup, fileName) {
        let form = new FormData();
        form.append('file', file, fileName);
        form.append('fileGroup', fileGroup);

        let requestOptions = this.setRequestOptions(null);
        requestOptions.headers['Content-Type'] = "multipart/form-data";

        try {
            let response = await axios.post(jsonUrls.upload, form, requestOptions);
            return this.processRequestResponse(response);
        }
        catch (exception) {
            return Promise.reject(exception.response.data);
        }
    },
    async get(url, requestOptions) {
        let options = this.setRequestOptions(requestOptions);
        try {
            let response = await axios.get(url, options);
            return this.processRequestResponse(response);
        }
        catch (exception) {
            return Promise.reject(exception.response.data);
        }
    },
    async post(url, data, requestOptions = null) {
        console.log(JSON.stringify(data))
        let options = this.setRequestOptions(requestOptions);
        try {
            let response = await axios.post(url, data, options);
            return this.processRequestResponse(response);
        }
        catch (exception) {
            return Promise.reject(exception.response.data);
        }
    },
    async put(url, data, requestOptions = null) {
        let options = this.setRequestOptions(requestOptions);
        try {
            let response = await axios.put(url, data, options);
            return this.processRequestResponse(response);
        }
        catch (exception) {
            return Promise.reject(exception.response.data);
        }
    },
    async delete(url, data = null, requestOptions = null) {
        let options = this.setRequestOptions(requestOptions);
        try {
            let response = await axios.delete(url, data, options);
            return this.processRequestResponse(response);
        }
        catch (exception) {
            return Promise.reject(exception.response.data);
        }
    },
    setRequestOptions(requestOptions) {
        if (!requestOptions || requestOptions == null)
            requestOptions = {};
        this.setToken();
        if (requestOptions.headers && requestOptions.headers != null) {
            requestOptions.headers['Authorization'] = 'Bearer ' + this.token;
        }
        else {
            requestOptions.headers = { 'Authorization': 'Bearer ' + this.token };
        }
        return requestOptions;
    },
    setToken() {
        let accountItem = localStorage.getItem('account');
        if (this.token != null && this.token != '' && accountItem != null)
            return;
        if (accountItem != undefined && accountItem != null) {
            let account = JSON.parse(accountItem);
            this.token = account.token;
        }
        if (this.token == null)
            this.token = '';
    },
    processRequestResponse(response) {
        var data = response.data;
        if (data.statusCode) {
            if (data.statusCode == 401) {
                let accountItem = localStorage.getItem('account');
                if (accountItem != undefined && accountItem != null) {
                    localStorage.removeItem('account')
                }
                if (!window.location.href.includes('admin/accounts/login'))
                    //router.push({ path: '/admin/accounts/login' })
                    window.location.href = "/admin/accounts/login"
                    
                else
                    return Promise.reject(data);
            }
            else if (data.statusCode > 300)
                return Promise.reject(data);
        }
            
        return Promise.resolve(data);
    }
}
export default baseRepository;