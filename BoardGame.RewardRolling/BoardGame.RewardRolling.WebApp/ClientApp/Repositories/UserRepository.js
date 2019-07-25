﻿import baseRepository from './BaseRepository';
import { jsonUrls } from './UrlConstants'
import string from '../Utils/StringExtensions';

export default {
    async login(loginModel) {
        try {
            var loginResult = await baseRepository.post(jsonUrls.user.login, loginModel);
            return Promise.resolve(loginResult);
        }
        catch (exception) {
            return Promise.reject(exception);
        }
    },
    async getCurrentUser() {
        try {
            var user = await baseRepository.get(jsonUrls.user.getCurrentUser);
            return Promise.resolve(user);
        }
        catch (exception) {
            return Promise.reject(exception);
        }
    },
    async createUser(model) {
        try {
            var user = await baseRepository.post(jsonUrls.user.createUser, model);
            return Promise.resolve(user);
        }
        catch (exception) {
            return Promise.reject(exception);
        }
    },
    async filter() {
        try {
            var users = await baseRepository.get(jsonUrls.user.filter);
            return Promise.resolve(users);
        }
        catch (exception) {
            return Promise.reject(exception);
        }
    },
    async delete(user) {
        try {
            var deleteResult = await baseRepository.delete(string.formatUnicorn(jsonUrls.user.delete, { id: user.id }));
            return Promise.resolve(deleteResult);
        }
        catch (exception) {
            return Promise.reject(exception);
        }
    }
}