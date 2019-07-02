export default {
    changeCurrentUser: (state, currentUser) => {
        state.currentUser.authenticated = true;
        state.currentUser.userName = currentUser.userName;
        state.currentUser.name = currentUser.name;
        state.currentUser.avatarUrl = currentUser.avatarUrl;
    }
}