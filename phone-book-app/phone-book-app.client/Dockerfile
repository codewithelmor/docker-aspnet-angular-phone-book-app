### STAGE 1: Build ###
FROM node:lts-alpine3.17 AS build
WORKDIR /app
COPY package.json package-lock.json ./
RUN npm install
COPY . .
RUN npm run build
### STAGE 2: Run ###
FROM nginx:alpine3.18
COPY nginx.conf /etc/nginx/nginx.conf
COPY --from=build /app/dist/phone-book-app.client/browser /usr/share/nginx/html
