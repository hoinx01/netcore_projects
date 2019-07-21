import baseRepository from './BaseRepository';
import { jsonUrls } from './UrlConstants'
import string from '../Utils/StringExtensions';

export default {
    async getByCityId(cityId) {
        try {
            console.log(cityId)
            var result = await baseRepository.get(jsonUrls.district.getDistrictByCityId, { params: { cityId: cityId }});
            return Promise.resolve(result);
        }
        catch (exception) {
            return Promise.reject(exception);
        }
    },
    async add(district) {
        try {
            var result = await baseRepository.post(jsonUrls.district.add, district);
            return Promise.resolve(result);
        }
        catch (exception) {
            return Promise.reject(exception);
        }
    },
    async update(district) {
        try {
            var result = await baseRepository.put(string.formatUnicorn(jsonUrls.district.update, { id: district.id }), district);
            return Promise.resolve(result);
        }
        catch (exception) {
            return Promise.reject(exception);
        }
    },
    async delete(district) {
        try {
            var deleteResult = await baseRepository.delete(string.formatUnicorn(jsonUrls.district.delete, { id: district.id }));
            return Promise.resolve(deleteResult);
        }
        catch (exception) {
            return Promise.reject(exception);
        }
    }
}