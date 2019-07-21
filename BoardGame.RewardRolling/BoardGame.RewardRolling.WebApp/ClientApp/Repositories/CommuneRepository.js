import baseRepository from './BaseRepository';
import { jsonUrls } from './UrlConstants'
import string from '../Utils/StringExtensions';

export default {
    async getByDistrictId(districtId) {
        try {
            var result = await baseRepository.get(jsonUrls.commune.getCommuneByDistrictId, { params: { districtId: districtId }});
            return Promise.resolve(result);
        }
        catch (exception) {
            return Promise.reject(exception);
        }
    },
    async add(commune) {
        try {
            var result = await baseRepository.post(jsonUrls.commune.add, commune);
            return Promise.resolve(result);
        }
        catch (exception) {
            return Promise.reject(exception);
        }
    },
    async update(commune) {
        try {
            var result = await baseRepository.put(string.formatUnicorn(jsonUrls.commune.update, { id: commune.id }), commune);
            return Promise.resolve(result);
        }
        catch (exception) {
            return Promise.reject(exception);
        }
    },
    async delete(commune) {
        try {
            var deleteResult = await baseRepository.delete(string.formatUnicorn(jsonUrls.commune.delete, { id: commune.id }));
            return Promise.resolve(deleteResult);
        }
        catch (exception) {
            return Promise.reject(exception);
        }
    }
}