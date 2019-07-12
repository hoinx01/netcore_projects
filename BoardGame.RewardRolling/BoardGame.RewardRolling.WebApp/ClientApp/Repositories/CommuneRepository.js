import baseRepository from './BaseRepository';
import { jsonUrls } from './UrlConstants'
import string from '../Utils/StringExtensions';

export default {
    async getByDistrictId(districtId) {
        try {
            var result = await baseRepository.get(jsonUrls.commune.getCommuneByDistrictId, { districtId: districtId });
            return Promise.resolve(result);
        }
        catch (exception) {
            return Promise.reject(exception);
        }
    }
}