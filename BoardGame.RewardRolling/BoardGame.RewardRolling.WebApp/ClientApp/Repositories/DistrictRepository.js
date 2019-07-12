import baseRepository from './BaseRepository';
import { jsonUrls } from './UrlConstants'
import string from '../Utils/StringExtensions';

export default {
    async getByCityId(cityId) {
        try {
            var result = await baseRepository.get(jsonUrls.district.getDistrictByCityId, {cityId: cityId});
            return Promise.resolve(result);
        }
        catch (exception) {
            return Promise.reject(exception);
        }
    }
}