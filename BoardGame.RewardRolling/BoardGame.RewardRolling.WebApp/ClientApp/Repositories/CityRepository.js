import baseRepository from './BaseRepository';
import { jsonUrls } from './UrlConstants'
import string from '../Utils/StringExtensions';

export default {
    async getAll() {
        try {
            var result = await baseRepository.get(jsonUrls.city.getAll);
            return Promise.resolve(result);
        }
        catch (exception) {
            return Promise.reject(exception);
        }
    },
    async add(city) {
        try {
            var result = await baseRepository.post(jsonUrls.city.add, city);
            return Promise.resolve(result);
        }
        catch (exception) {
            return Promise.reject(exception);
        }
    },
    async update(city) {
        try {
            var result = await baseRepository.put(string.formatUnicorn(jsonUrls.city.update, {id: city.id}), city);
            return Promise.resolve(result);
        }
        catch (exception) {
            return Promise.reject(exception);
        }
    },
    async delete(city) {
        try {
            var deleteResult = await baseRepository.delete(string.formatUnicorn(jsonUrls.city.delete, { id: city.id }));
            return Promise.resolve(deleteResult);
        }
        catch (exception) {
            return Promise.reject(exception);
        }
    }
}