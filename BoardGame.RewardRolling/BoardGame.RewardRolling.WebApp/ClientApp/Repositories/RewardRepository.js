import baseRepository from './BaseRepository';
import { jsonUrls } from './UrlConstants'
import String from '../Utils/StringExtensions';

export default {
    async filter(filterModel)
    {
        try
        {
            var filterResult = await baseRepository.get(jsonUrls.reward.filter, { params: filterModel });
            return Promise.resolve(filterResult);
        }
        catch (exception)
        {
            return Promise.reject(exception);
        }
    },
    async add(reward)
    {
        try
        {
            var addResult = await baseRepository.post(jsonUrls.reward.add, reward);
            return Promise.resolve(addResult);
        }
        catch (exception)
        {
            return Promise.reject(exception);
        }
    },
    async update(reward) {
        try {
            var updateResult = await baseRepository.post(String.formatUnicorn(jsonUrls.reward.update, { id: reward.id }), reward);
            return Promise.resolve(updateResult);
        }
        catch (exception) {
            return Promise.reject(exception);
        }
    },
    async delete(reward) {
        try {
            var deleteResult = await baseRepository.post(String.formatUnicorn(jsonUrls.reward.delete, { id: reward.id }), reward);
            return Promise.resolve(deleteResult);
        }
        catch (exception) {
            return Promise.reject(exception);
        }
    }
}