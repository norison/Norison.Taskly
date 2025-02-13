FROM node:18-alpine AS build
WORKDIR /app
COPY ./src/front-end/taskly-web-client/package.json .
RUN npm install
COPY ./src/front-end/taskly-web-client .
RUN npm run build

FROM nginx:stable-alpine AS production
COPY --from=build /app/dist /usr/share/nginx/html
CMD ["nginx", "-g", "daemon off;"]