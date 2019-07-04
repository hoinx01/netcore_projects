import baseRepository from './BaseRepository';
import { jsonUrls } from './UrlConstants'
import string from '../Utils/StringExtensions';

export default {
    async filter(filterModel) {
        try {
            var filterResult = await baseRepository.get(jsonUrls.campaign.filter, { params: filterModel });
            return Promise.resolve(filterResult);
        }
        catch (exception) {
            return Promise.reject(exception);
        }
    },
    async getById(id) {
        try {
            var result = await baseRepository.get(string.formatUnicorn(jsonUrls.campaign.getById, { id: id }));
            return Promise.resolve(result);
        }
        catch (exception) {
            return Promise.reject(exception);
        }
    },
    async add(campaign) {
        try {
            console.log(campaign)
            var addResult = await baseRepository.post(jsonUrls.campaign.add, campaign);
            return Promise.resolve(addResult);
        }
        catch (exception) {
            return Promise.reject(exception);
        }
    },
    async update(campaign) {
        try {
            var updateResult = await baseRepository.put(string.formatUnicorn(jsonUrls.campaign.update, { id: campaign.id }), campaign);
            return Promise.resolve(updateResult);
        }
        catch (exception) {
            return Promise.reject(exception);
        }
    },
    async delete(campaign) {
        try {
            var deleteResult = await baseRepository.delete(string.formatUnicorn(jsonUrls.campaign.delete, { id: campaign.id }));
            return Promise.resolve(deleteResult);
        }
        catch (exception) {
            return Promise.reject(exception);
        }
    }
}