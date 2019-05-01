FROM node:latest

# ENV http_proxy http://FINANS%5CT32465:14onOZ05!@10.81.105.12:8080
# ENV https_proxy http://FINANS%5CT32465:14onOZ04!@10.81.105.12:8080

COPY ./infrastructure/dataseeds/wait-for-it.sh /wait-for-it.sh
RUN chmod +x /wait-for-it.sh

WORKDIR /usr/src/app

COPY package*.json ./

RUN npm install
COPY . .

EXPOSE 8080
CMD [ "npm", "start" ]