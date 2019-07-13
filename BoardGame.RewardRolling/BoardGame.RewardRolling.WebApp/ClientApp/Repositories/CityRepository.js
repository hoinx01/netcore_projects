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
    }
}