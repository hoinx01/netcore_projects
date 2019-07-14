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
    }
}