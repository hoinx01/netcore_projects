import baseRepository from './BaseRepository';
import { jsonUrls } from './UrlConstants'
import string from '../Utils/StringExtensions';

export default {
    async filter(filterModel) {
        try {
            var filterResult = await baseRepository.get(jsonUrls.customer.filter, { params: filterModel });
            return Promise.resolve(filterResult);
        }
        catch (exception) {
            return Promise.reject(exception);
        }
    },
    async getById(id) {
        try {
            var result = await baseRepository.get(string.formatUnicorn(jsonUrls.customer.getById, { id: id }));
            return Promise.resolve(result);
        }
        catch (exception) {
            return Promise.reject(exception);
        }
    },
    async exportExcel(filterModel) {
        try {
            var filterResult = await baseRepository.get(jsonUrls.customer.exportExcel, { params: filterModel });
            return Promise.resolve(filterResult);
        }
        catch (exception) {
            return Promise.reject(exception);
        }
    }
}